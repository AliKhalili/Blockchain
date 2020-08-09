using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Server
{
    public class ActionFactory : IActionFactory
    {
        private readonly Dictionary<string, IAction> _actions;
        public ActionFactory()
        {
            _actions = new Dictionary<string, IAction>();
            var type = typeof(IAction);
            var actions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !type.IsAbstract && !type.IsInterface).ToList();

            foreach (var action in actions)
            {
                _actions.Add(action.Name, (IAction)Activator.CreateInstance(type));
            }
        }
        public IAction Create(HttpListenerRequest request)
        {
            var actionName = request.Url.LocalPath;
            if (_actions.ContainsKey(actionName))
                return _actions[actionName];

            return new NotFoundAction();
        }
    }
}