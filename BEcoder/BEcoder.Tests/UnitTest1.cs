using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace BEcoder.Tests
{
	public class UnitTest1
	{
		private class TestData : IEnumerable<Object[]>
		{
			public IEnumerator<Object[]> GetEnumerator()
			{
				yield return new Object[]
				{
					new[]
					{
						"4",
						"4578 5 1256 4323 1897 3244 5678",
						"1256 2 4578 1897",
						"4323 4 5678 6547 9766 9543",
						"9766 1 3624"
					},
					new[] {"6"}
				};
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}

		[Theory]
		[ClassData(typeof(TestData))]
		public void SolutionTest(String[] inputStrings, String[] expectedOutput)
		{
			using (var inStream = new MemoryStream())
			using (var outStream = new MemoryStream())
			using (var testWriter = new StreamWriter(inStream) {AutoFlush = true})
			using (var solutionInput = new StreamReader(inStream))
			using (var solutionOutput = new StreamWriter(outStream) {AutoFlush = true})
			{
				foreach (String s in inputStrings)
					testWriter.WriteLine(s);

				inStream.Position = 0;
				Solution.Solve(solutionInput, solutionOutput);

				outStream.Position = 0;
				using (var testReader = new StreamReader(outStream))
				{
					String result = testReader
						.ReadToEnd()
						.Trim(Environment.NewLine.ToCharArray());

					Assert.Equal(String.Join(Environment.NewLine, expectedOutput), result);
				}
			}
		}
	}
}