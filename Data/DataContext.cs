using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enums;
namespace RpgApi.Data
{
    public class DataContext: DbContext
    {
                   public DataContext(DbContextOptions<DataContext> options): base(options)
                   {

                   }

                   public DbSet<Personagem> Personagens {get; set;}
                   public DbSet<Arma> Armas {get; set;}
                   public DbSet<Usuario> Usuarios {get; set;}
                   public DbSet<Habilidade> Habilidades {get; set;}
                   public DbSet<PersonagemHabilidade> PersonagemHabilidades{get; set;}
                   public DbSet<Disputa> Disputas {get; set;}

                   protected override void OnModelCreating(ModelBuilder modelBuilder) // Só pode ter um overrride com OnModelCreating
                   {             
                       
                        modelBuilder.Entity<Usuario>().HasData
                        (
                            new Usuario(){Id = 1, Username="Kakika" }
                        );
                                    
                       modelBuilder.Entity<Personagem>().HasData 
                       (
                            new Personagem() { Id = 1, Nome = "Frodo" }, //Frodo Cavaleiro             
                            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},     
                            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
                            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
                            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
                            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
                            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
                       );
                       
                        modelBuilder.Entity<Arma>().HasData
                        (
                            new Arma(){ Id = 1, Nome = "Arco e Flecha", Dano = 30, PersonagemId=1},
                            new Arma(){Id = 2, Nome = "Espada", Dano = 50, PersonagemId=2},
                            new Arma(){Id = 3, Nome = "Machado", Dano = 60, PersonagemId=3}  
                        ); 

                        modelBuilder.Entity<Habilidade>().HasData
                        (
                            new Habilidade(){Id = 1, Nome="Adormecer", Dano = 39},
                            new Habilidade(){Id = 2, Nome = "Congelar", Dano = 41},
                            new Habilidade(){Id = 3, Nome="Hipnotizar", Dano=37}
                        );

                        modelBuilder.Entity<PersonagemHabilidade>().HasKey(ph => new{ph.PersonagemId, ph.HabilidadeId});
                        modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");


                   }
                   
        

    }
}   