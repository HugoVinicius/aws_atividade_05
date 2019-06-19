using APIProdutos.Data;
using APIProdutos.Models.GraphQL;
using GraphQL.Types;

namespace APIProdutos.Models.Queries
{
    public class EatMoreQuery : ObjectGraphType
    {
        public EatMoreQuery(ApplicationDbContext db)
        {

            Field<ListGraphType<ProdutoType>>(
                "produtos",
                resolve: context =>
                {
                    var produtos = db
                    .Produtos;
                    return produtos;
                });

        }
    }
}