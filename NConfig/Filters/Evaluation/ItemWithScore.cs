namespace ByContext.Filters.Evaluation
{
    public struct ItemWithScore
    {
        public readonly ItemEvaluation Item;
        public readonly int Score;

        public ItemWithScore(ItemEvaluation item, int score)
        {
            Item = item;
            Score = score;
        }
    }
}