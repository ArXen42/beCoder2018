using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BEcoder.Tests
{
	public class UnitTest1
	{
		private class TestData : IEnumerable<Object[]>
		{
			public IEnumerator<Object[]> GetEnumerator()
			{
				Object[] GetDefaultTestData() => new Object[]
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

				Object[] GetGeneratedTestData()
				{
					String[] values = new String[101];
					Int32 initial = 100;
					values[0] = "100";
					for (Int32 i = 0; i < 100; i++)
					{
						StringBuilder sb = new StringBuilder().Append($"{i} 99 ");
						for (Int32 j = 0; j < 99; j++)
						{
							++initial;
							sb.Append($"{initial} ");
						}
						values[i + 1] = sb.ToString();
					}
					return new Object[]
					{
						values,
						new[] {"9900"}
					};
				}

				yield return GetDefaultTestData();
				yield return GetGeneratedTestData();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
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