using System;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace BlazorChat.Data
{
	public class UserService
	{
		private Dictionary<string, string> _users = new();

		public void Add(string connectionId, string username)
		{
			if(!_users.ContainsValue(username))
				_users.Add(connectionId, username);
		}

		public void RemoveByName(string username)
		{
			if (_users.ContainsValue(username))
			{
				var item = _users.First(user => user.Value == username);
				_users.Remove(item.Key);
			}
        }

        public string GetConnectionIdByName(string username)
		{
			if (_users.ContainsValue(username))
			{
				var item = _users.First(user => user.Value == username);
				return item.Key;
			}
			return "";
		}

		public IEnumerable<(string ConnectionId, string Username)> GetAll()
		{
			return _users.Keys.ToList().Select(key => (key, _users[key]));
		}
	}
}

