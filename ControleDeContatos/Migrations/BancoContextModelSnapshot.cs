using ControleDeContatos.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControleDeContatos.Models.ContatoModel", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Celular")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Contatos");
            });

            modelBuilder.Entity("ControleDeContatos.Models.UsuarioModel", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime?>("DataAtualizacao")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("DataCadastro")
                    .HasColumnType("datetime2");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Login")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Perfil")
                    .HasColumnType("int");

                b.Property<string>("Senha")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Usuarios");
            });
#pragma warning restore 612, 618
        }
    }
}