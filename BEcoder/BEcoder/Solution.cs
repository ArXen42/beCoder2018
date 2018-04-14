using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BEcoder
{
	public static class Solution
	{
		public static void Solve(TextReader input, TextWriter output)
		{
			String firstLine = input.ReadLine();
			Int32 rootFriendsCount = Int32.Parse(firstLine);

			HashSet<Int32> rootFriends = new HashSet<Int32>();
			HashSet<Int32> friends = new HashSet<Int32>();
			
			for (Int32 i = 0; i < rootFriendsCount; i++)
			{
				// ReSharper disable once PossibleNullReferenceException
				var rootFriendLine = input.ReadLine()
					.Split(' ')
					.Select(Int32.Parse)
					.ToArray();

				Int32 rootFriendId = rootFriendLine[0];
				Int32 friendsCount = rootFriendLine[1];

				rootFriends.Add(rootFriendId);
				for (Int32 f = 0; f < friendsCount; f++)
					friends.Add(rootFriendLine[f + 2]);
			}
			
			friends.ExceptWith(rootFriends);
			
			output.WriteLine(friends.Count);
		}
	}
}