using Microsoft.AspNetCore.Mvc;
using APIProdutos.Data;
using System.Threading.Tasks;
using APIProdutos.Models.Queries;
using GraphQL.Types;
using GraphQL;

[Route("graphql")]
public class GraphQLController : Controller
{
    private readonly ApplicationDbContext _db;

    public GraphQLController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
    {
        var inputs = query.Variables.ToInputs();

        var schema = new Schema()
        {
            Query = new EatMoreQuery(_db)
        };

        var result = await new DocumentExecuter().ExecuteAsync(_ =>
        {
            _.Schema = schema;
            _.Query = query.Query;
            _.OperationName = query.OperationName;
            _.Inputs = inputs;
        }).ConfigureAwait(false);

        if (result.Errors?.Count > 0)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}