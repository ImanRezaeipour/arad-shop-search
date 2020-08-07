using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Common.Domain.Model.Common;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneSearch.Core.Model;
using LuceneSearch.Core.Utils;
using SpellChecker.Net.Search.Spell;

namespace LuceneSearch.Core
{
    public class CreateIndex
    {
        //private readonly AutoComplete _autoComplete;

        static readonly Lucene.Net.Util.Version _version = Lucene.Net.Util.Version.LUCENE_30;
        public static SpellChecker.Net.Search.Spell.SpellChecker spellChecker;
        public Document MapPostToDocument(SearchResult post)
        {
            var postDocument = new Document();
            postDocument.Add(new Field("Id", post.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            var titleField = new Field("Title", post.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            titleField.Boost = 3;
            postDocument.Add(titleField);
            return postDocument;
        }

        internal void UppdateFullTextIndex(List<SearchResult> dataList, string path)
        {
            var directory = FSDirectory.Open(new DirectoryInfo(path));
            var analyzer = new StandardAnalyzer(_version);
            //var analyzer = new WhitespaceAnalyzer();
            using (var writer = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.DeleteAll();
                foreach (var post in dataList)
                {
                    writer.AddDocument(MapPostToDocument(post));
                }

                writer.Optimize();
                writer.Commit();
                writer.Dispose();
                directory.Dispose();
                //Change here
            }

            var indexReader = IndexReader.Open(FSDirectory.Open(path), readOnly: true);

            // Create the SpellChecker
            //Directory d = new Directory();d.Delete(path + "\\Spell");
            if (spellChecker == null)
                spellChecker = new SpellChecker.Net.Search.Spell.SpellChecker(FSDirectory.Open(path + "\\Spell"));

            // Create SpellChecker Index
            spellChecker.ClearIndex();
            spellChecker.IndexDictionary(new LuceneDictionary(indexReader, "Title"));

        }

        public void CreateFullTextIndex(IEnumerable<SearchResult> dataList, string path)
        {
            var directory = FSDirectory.Open(new DirectoryInfo(path));
            var analyzer = new StandardAnalyzer(_version);
            //var analyzer = new WhitespaceAnalyzer();
            using (var writer = new IndexWriter(directory, analyzer, create: true, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var post in dataList)
                {
                    writer.AddDocument(MapPostToDocument(post));
                }

                writer.Optimize();
                writer.Commit();
                writer.Dispose();
                directory.Dispose();
                //change here
            }

            var indexReader = IndexReader.Open(FSDirectory.Open(path), readOnly: true);

            // Create the SpellChecker
            //Directory d = new Directory();d.Delete(path + "\\Spell");
            spellChecker = new SpellChecker.Net.Search.Spell.SpellChecker(FSDirectory.Open(path + "\\Spell"));

            // Create SpellChecker Index
            spellChecker.ClearIndex();
            spellChecker.IndexDictionary(new LuceneDictionary(indexReader, "Title"));
        }

        internal List<string> SuggestSimilar(string prefix, int maxItems)
        {
            var items = spellChecker.SuggestSimilar(prefix, maxItems, null, null, true).ToList();
            if (spellChecker.Exist(prefix))
                items.Add(prefix);
            return items;
        }



    }
}
