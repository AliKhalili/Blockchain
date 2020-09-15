using SHPA.Blockchain.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SHPA.Blockchain.Server.Actions.Custom;

namespace SHPA.Blockchain.Server
{
    public class ActionFactory : IActionFactory
    {
        private readonly Dictionary<string, IAction> _actions;
        public ActionFactory(IServiceProvider serviceProvider)
        {
            _actions = new Dictionary<string, IAction>();
            var type = typeof(IAction);
            var actions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface).ToList();

            foreach (var action in actions)
            {
                _actions.Add(action.Name.Substring(0, action.Name.Length - "action".Length).ToLower(), (IAction)serviceProvider.GetService(action));
            }
        }
        public IAction Create(HttpListenerRequest request)
        {
            var actionName = request.Url.LocalPath.Substring(1).ToLower();
            if (_actions.ContainsKey(actionName))
                return _actions[actionName];

            return new NotFoundAction();
        }
    }
}