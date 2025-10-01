using EfCrudApp.Data;
using EfCrudApp.Models;
using Microsoft.EntityFrameworkCore;

using var db = new AppDbContext();
db.Database.EnsureCreated();

Console.WriteLine("=== Sistema de Gerenciamento de Produtos ===");

while (true)
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("1 - Adicionar categoria");
    Console.WriteLine("2 - Adicionar produto");
    Console.WriteLine("3 - Listar produtos");
    Console.WriteLine("4 - Atualizar produto");
    Console.WriteLine("5 - Deletar produto");
    Console.WriteLine("0 - Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Nome da categoria: ");
            var nomeCat = Console.ReadLine() ?? "";
            Console.Write("Descrição: ");
            var desc = Console.ReadLine();
            db.Categories.Add(new Category { Name = nomeCat, Description = desc });
            db.SaveChanges();
            Console.WriteLine("Categoria adicionada!");
            break;

        case "2":
            Console.Write("Nome do produto: ");
            var nomeProd = Console.ReadLine() ?? "";
            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Estoque: ");
            int estoque = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("CategoriaId: ");
            int catId = int.Parse(Console.ReadLine() ?? "0");

            db.Products.Add(new Product
            {
                Name = nomeProd,
                Price = preco,
                Stock = estoque,
                CategoryId = catId
            });
            db.SaveChanges();
            Console.WriteLine("Produto adicionado!");
            break;

        case "3":
            var produtos = db.Products.Include(p => p.Category).ToList();
            Console.WriteLine("\n=== Produtos ===");
            foreach (var p in produtos)
                Console.WriteLine($"{p.Id} - {p.Name} | R${p.Price} | Estoque: {p.Stock} | Categoria: {p.Category?.Name}");
            break;

        case "4":
            Console.Write("ID do produto a atualizar: ");
            int idUp = int.Parse(Console.ReadLine() ?? "0");
            var prodUp = db.Products.Find(idUp);
            if (prodUp == null)
            {
                Console.WriteLine("Produto não encontrado.");
                break;
            }
            Console.Write("Novo nome: ");
            prodUp.Name = Console.ReadLine() ?? prodUp.Name;
            Console.Write("Novo preço: ");
            prodUp.Price = decimal.Parse(Console.ReadLine() ?? prodUp.Price.ToString());
            db.SaveChanges();
            Console.WriteLine("Produto atualizado!");
            break;

        case "5":
            Console.Write("ID do produto a deletar: ");
            int idDel = int.Parse(Console.ReadLine() ?? "0");
            var prodDel = db.Products.Find(idDel);
            if (prodDel == null)
            {
                Console.WriteLine("Produto não encontrado.");
                break;
            }
            db.Products.Remove(prodDel);
            db.SaveChanges();
            Console.WriteLine("Produto deletado!");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}
