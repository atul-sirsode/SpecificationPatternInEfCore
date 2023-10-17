using SpecificationPatternInEfCore.Entity;

namespace SpecificationPatternInEfCore.Specifications;

public class GameSpecification : Specification<Game>
{
    public GameSpecification() : base(g => g.Name == "Action Game")
    {
        AddIncludes(x=>x.Genre!);
        AddOrderBy(x=>x.id);
    }
}
