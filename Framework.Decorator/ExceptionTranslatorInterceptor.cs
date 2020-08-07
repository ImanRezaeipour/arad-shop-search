using Castle.Core.Logging;
using Castle.DynamicProxy;
using Framework.Core.ApplicationException;
using Framework.Core.ExceptionHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Framework.Decorator
{
    public class ExceptionTranslatorInterceptor : IInterceptor
    {

        #region =================== Private Variable ====================== 
        private readonly IExceptionTranslator _Translator;
        private static Dictionary<string, string> KeyCheckers = new Dictionary<string, string>();

        class Response
        {
            public Response(string message)
            {
                this.Message = message;
            }
            public string Message { get; set; }
        }

        #endregion

        #region =================== Public Property ======================= 
        public ILogger Logger { get; set; }

        #endregion

        #region =================== Public Constructor ==================== 
        public ExceptionTranslatorInterceptor(IExceptionTranslator translator)
        {
            _Translator = translator;
            Logger = NullLogger.Instance;
        }
        #endregion

        #region =================== Private Methods ======================= 
        #endregion

        #region =================== Public Methods ======================== 
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Logger.Fatal(ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new Response(ex.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "Translate" //TODO: Translate
                };
                throw new HttpResponseException(resp);
            }

            catch (ArgumentException ex)
            {
                Logger.Fatal(ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {

                    Content = new StringContent(JsonConvert.SerializeObject(new Response(ex.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "Translate" //TODO: Translate
                };
                throw new HttpResponseException(resp);
            }

            catch (ViewableException ex)
            {
                Logger.Fatal(ex.ErrorCode.ToString());
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new Response(ex.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "Translate" //TODO: Translate
                };
                throw new HttpResponseException(resp);
            }
            catch (DbUpdateException ex)
            {
                Logger.Fatal(ex.ToString());
                var SourceError = ex.ToString().ToLower();
                foreach (var item in KeyCheckers)
                {
                    if (SourceError.Contains(item.Key))
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(item.Value, Encoding.UTF8, "application/json"),
                            ReasonPhrase = "Translate" //TODO: Translate
                        };
                        throw new HttpResponseException(resp);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString());
                throw;
            }
        }

        #endregion

    }
}
