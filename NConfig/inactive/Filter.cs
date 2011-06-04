//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace NConfig.ValueSelector
//{
//    public class Filter : IFilterPolicy
//    {
//        private readonly ILoggerFacade _logger = new OutputLogger();

//        public const string All = "All";

//        public IEnumerable<IHaveConfigContext> DoFilter(IEnumerable<IHaveConfigContext> items, IDictionary<string, string> runtimeContext)
//        {
//            List<IHaveConfigContext> result = new List<IHaveConfigContext>();

//            foreach (IHaveConfigContext haveConfigItem in items)
//            {
//                _logger.Log(string.Format("Processing item: {0} with context: {1}.", haveConfigItem.ToString(), haveConfigItem.Context.ToFlatString()));

//                bool exclude = false;
//                foreach (KeyValuePair<string, string> runtimeContextItem in runtimeContext)
//                {
//                    _logger.Log(string.Format("Processing context item with key:{0} and value: {1}.", runtimeContextItem.Key, runtimeContextItem.Value));

                    
//                    string contextKeyReference = null;

//                    haveConfigItem.Context.TryGetValue(runtimeContextItem.Key, out contextKeyReference);

//                    if (contextKeyReference != null)
//                    {
//                        if (runtimeContextItem.Value == contextKeyReference)
//                        {
//                            _logger.Log("Item selected because it has a specific reference to the context key");
//                        }
//                        else if (runtimeContextItem.Value == Filter.All)
//                        {
//                            _logger.Log("Item has an 'All' reference to the context key");
//                        }
//                    }
//                    else
//                    {
//                        _logger.Log("Item doesn't have any reference to the context key.");
//                        exclude = true;
//                    }
//                }
//                if (exclude)
//                {
//                    _logger.Log("item filtered.");
//                }
//                else
//                {
//                    _logger.Log("item selected.");
//                    result.Add(haveConfigItem);
//                }
//            }

//            return result;

//            IEnumerable<IHaveConfigContext> query = items.Select(x=>x);
//            foreach (KeyValuePair<string, string> item in runtimeContext)
//            {
//                query = this.AppendQueryForAssociation(query, item.Key,item.Value);
//            }

//            return query;
//        }

//        private IEnumerable<IHaveConfigContext> AppendQueryForAssociation(IEnumerable<IHaveConfigContext> sourceQuery, string name, string value)
//        {
//            IEnumerable<IHaveConfigContext> result = from item in sourceQuery
//                                                     where item.Context.Any(x => x.Key == name && x.Value == value)
//                                                     ||
//                                                     !item.Context.ContainsKey(name)
//                                                     select item;
//            return result;
//        }
//    }
//}
