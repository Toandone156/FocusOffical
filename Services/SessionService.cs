using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace FocusOffical.Services
{
    public interface ISessionService
    {
        T GetSessionValue<T>(HttpContext context, string sessionKey);
        void AddToSession(HttpContext context, string sessionKey, object data);
        void DeleteSession(HttpContext context, string sessionKey);
    }

    public class SessionService : ISessionService
    {
        public T GetSessionValue<T>(HttpContext context, string sessionKey)
        {
            string value = context.Session.GetString(sessionKey);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public void AddToSession(HttpContext context, string sessionKey, object data)
        {
            context.Session.SetString(sessionKey, JsonConvert.SerializeObject(data));
        }

        public void DeleteSession(HttpContext context, string sessionKey)
        {
            context.Session.Remove(sessionKey);
        }
    }
}
