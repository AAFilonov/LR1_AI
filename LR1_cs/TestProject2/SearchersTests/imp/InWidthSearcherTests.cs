using LR1_AI_cs.ai;

namespace TestProject2.SearchersTests
{
    public class InWidthSearcherTests : AbstractSearcherTests
    {
        public override ISolutionFinder getSearcher()
        {
            return new InWidthSearcher();
        }
    }
}