using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Domain.Model.Common;
using Contrib.Regex;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Shingle;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Search.Spans;
using Lucene.Net.Store;
using LuceneSearch.Core.Model;
using LuceneSearch.Core.Utils;

namespace LuceneSearch.Core
{
    public class AutoComplete
    {
        private static IndexSearcher _searcher;
        //private static IndexSearcher _spellChecker;
        private readonly CreateIndex _createIndex;
        public AutoComplete(CreateIndex createIndex)
        {
            _createIndex = createIndex;
        }

        public void RenewSearcherObject(string indexPath)
        {
            _searcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(indexPath)), true);
        }
        /// <summary>
        /// Get terms starting with the given prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="maxItems"></param>
        /// <returns></returns>
        public SearchModel GetTermsScored(string indexPath, string prefix, int maxItems = 100)
        {
            if (_searcher == null)
                _searcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(indexPath)), true);

            var resultsList = new List<SearchResult>();

            var searchModel = new SearchModel();
            if (string.IsNullOrWhiteSpace(prefix))
                return searchModel;

            prefix = prefix.ApplyCorrectYeKe();

            var results = SearcheItem(prefix, maxItems);

            var suggestFlag = false;

            if (results.TotalHits == 0)
            {
                suggestFlag = true;
                results = SearcheItem(prefix, maxItems, (float)0.6);
                if (results.TotalHits == 0)
                {
                    results = SearcheItem(prefix, maxItems, (float)1,Occur.SHOULD);
                }

                if (results.TotalHits == 0)
                {
                    results = SearcheItem(prefix, maxItems, (float)0.6, Occur.SHOULD);
                }


            }

            foreach (var doc in results.ScoreDocs)
            {
                resultsList.Add(new SearchResult
                {
                    Title = _searcher.Doc(doc.Doc).Get("Title"),
                    Id = Guid.Parse(_searcher.Doc(doc.Doc).Get("Id"))
                });
            }

            if (suggestFlag)
            {
                searchModel.Suggests = new List<List<string>>();

                if (prefix.Contains(" "))
                {
                    foreach (string s in prefix.Split(' '))
                    {
                        if (string.IsNullOrWhiteSpace(s))
                            continue;
                        var suggests = _createIndex.SuggestSimilar(s, maxItems);
                        
                        if (results.TotalHits > 0)
                        {
                            suggests = SuggestReOrder(suggests, ref resultsList);
                        }

                        searchModel.Suggests.Add(suggests);
                    }
                }
                else
                {
                    var suggests = _createIndex.SuggestSimilar(prefix, maxItems);
                    
                    if (results.TotalHits > 0)
                    {
                        suggests = SuggestReOrder(suggests, ref resultsList);
                    }

                    searchModel.Suggests.Add(suggests);
                }
            }

            searchModel.Results = resultsList;
            return searchModel;
        }

        private List<string> SuggestReOrder(List<string> suggests, ref List<SearchResult> results,string term=null)
        {
            var total = results.Count;
            var textResults = results.Select(a => a.Title).ToList();
            var scores = new List<SearchSuggestModel>();

            foreach (var item in suggests)
            {
                var score = textResults.Where(x => x.Contains(item)).Count();
                scores.Add(new SearchSuggestModel { Score = score, Suggest = item });
            }
            if(term!=null && textResults.Any(a=>a.Contains(term)))
                scores.Add(new SearchSuggestModel { Score = total+1, Suggest = term });


            return scores.OrderByDescending(a => a.Score).Select(a => a.Suggest).ToList();
        }

        private TopFieldDocs SearcheItem(string prefix, int maxItems, float similarity = 1, Occur occur= Occur.MUST)
        {
            TopFieldDocs results = null;
            if (prefix.Contains(" "))

            {

                if (similarity >= 1)
                {
                    var booleanQuery1 = new BooleanQuery();
                    var booleanQuery = new BooleanQuery();

                    var termList = prefix.Split(' ');
                    foreach (string s in termList)
                    {
                        if (string.IsNullOrWhiteSpace(s))
                            continue;
                        if (s.Contains("آ") || s.Contains("ا"))
                        {
                            var str = s.Replace("آ", "ا");
                            var booleanQueryA = new BooleanQuery();
                            booleanQueryA.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);
                            var str2 = s.Replace("ا","آ");
                            booleanQueryA.Add(new TermQuery(new Term("Title", str2)), Occur.SHOULD);
                            booleanQuery1.Add(booleanQueryA, Occur.MUST);
                        }
                        else
                            booleanQuery1.Add(new TermQuery(new Term("Title", s)), Occur.MUST);

                    }

                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا").Replace(" ",string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);

                        var str2 = prefix.Replace("ا", "آ").Replace(" ", string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str2)), Occur.SHOULD);
                    }
                    else
                    {
                        var str = prefix.Replace(" ", string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);
                    }

                    booleanQuery.Add(booleanQuery1, Occur.SHOULD);

                    results = _searcher.Search(booleanQuery, null, maxItems, Sort.RELEVANCE);

                }
                else
                {
                    var booleanQuery1 = new BooleanQuery();
                    foreach (string s in prefix.Split(' '))
                    {
                        var sim = (float)0.6;
                        if (s.Length >= 7)
                            sim = (float)0.7;
                        else
                            switch (s.Length)
                            {
                                case 1:
                                case 2:
                                    sim = (float)0.5;
                                    break;
                                default:
                                    break;
                            }
                        if (s.Contains("آ") || s.Contains("ا"))
                        {
                            var str = s.Replace("آ", "ا");
                            var booleanQueryA = new BooleanQuery();
                            booleanQueryA.Add(new FuzzyQuery(new Term("Title", str),sim), Occur.SHOULD);
                            var str2 = s.Replace("ا", "آ");
                            booleanQueryA.Add(new FuzzyQuery(new Term("Title", str2),sim), Occur.SHOULD);
                            booleanQuery1.Add(booleanQueryA, occur);
                        }
                        else
                            booleanQuery1.Add(new FuzzyQuery(new Term("Title", s), (float)sim), occur);
                    }

                    
                    var booleanQuery = new BooleanQuery();

                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا").Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str), (float)0.80), Occur.SHOULD);

                        var str2 = prefix.Replace("ا", "آ").Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str2), (float)0.80), Occur.SHOULD);
                    }
                    else
                    {
                        var str = prefix.Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str), (float)0.80), Occur.SHOULD);
                    }

                    booleanQuery.Add(booleanQuery1, Occur.SHOULD);

                    results = _searcher.Search(booleanQuery, null, maxItems, Sort.RELEVANCE);

                }

                //                var booleanQuery = new BooleanQuery() {
                //    new BooleanClause(new TermQuery(new Term("Title", prefix)), Occur.SHOULD),
                //    new BooleanClause(new MultiPhraseQuery() {
                //        new Term("field", "microsoft"),
                //        new Term("field", "office")
                //    }, Occur.SHOULD)
                //};

            }

            //single term query

            else
            {
                if (similarity >= 1)
                {
                    var booleanQuery1 = new BooleanQuery();

                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا");
                        var booleanQueryA = new BooleanQuery();
                        booleanQueryA.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);
                        var str2 = prefix.Replace("ا", "آ");
                        booleanQueryA.Add(new TermQuery(new Term("Title", str2)), Occur.SHOULD);
                        booleanQuery1.Add(booleanQueryA, Occur.SHOULD);
                    }
                    else
                    {

                        TermQuery query = new TermQuery(new Term("Title", prefix));
                        booleanQuery1.Add(query, Occur.SHOULD);
                    }


                    var booleanQuery = new BooleanQuery();

                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا").Replace(" ", string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);

                        var str2 = prefix.Replace("ا", "آ").Replace(" ", string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str2)), Occur.SHOULD);
                    }
                    else
                    {
                        var str = prefix.Replace(" ", string.Empty);
                        booleanQuery.Add(new TermQuery(new Term("Title", str)), Occur.SHOULD);
                    }

                    booleanQuery.Add(booleanQuery1, Occur.SHOULD);


                    results = _searcher.Search(booleanQuery, null, maxItems, Sort.RELEVANCE);
                }
                else
                {

                    var sim = (float)0.6;
                    if (prefix.Length >= 4)
                        sim = (float)0.7;
                    else
                        switch (prefix.Length)
                        {
                            case 1:
                            case 2:
                                sim = (float)0.5;
                                break;
                            default:
                                break;
                        }

                    var booleanQuery1 = new BooleanQuery();


                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا");
                        var booleanQueryA = new BooleanQuery();
                        booleanQueryA.Add(new FuzzyQuery(new Term("Title", str), sim), Occur.SHOULD);
                        var str2 = prefix.Replace("ا", "آ");
                        booleanQueryA.Add(new FuzzyQuery(new Term("Title", str2), sim), Occur.SHOULD);
                        booleanQuery1.Add(booleanQueryA, Occur.SHOULD);
                    }
                    else
                    {
                        var query = new FuzzyQuery(new Term("Title", prefix), (float)sim);
                        booleanQuery1.Add(query, Occur.SHOULD);
                    }

                    var booleanQuery = new BooleanQuery();

                    if (prefix.Contains("آ") || prefix.Contains("ا"))
                    {
                        var str = prefix.Replace("آ", "ا").Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str), (float)0.70), Occur.SHOULD);

                        var str2 = prefix.Replace("ا", "آ").Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str2), (float)0.70), Occur.SHOULD);
                    }
                    else
                    {
                        var str = prefix.Replace(" ", string.Empty);
                        booleanQuery.Add(new FuzzyQuery(new Term("Title", str), (float)0.70), Occur.SHOULD);
                    }

                    booleanQuery.Add(booleanQuery1, Occur.SHOULD);



                    results = _searcher.Search(booleanQuery, null, maxItems, Sort.RELEVANCE);
                }

            }
            return results;
        }
    }
}
