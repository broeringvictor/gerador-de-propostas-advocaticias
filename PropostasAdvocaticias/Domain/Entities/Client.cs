using System.ComponentModel.DataAnnotations;

namespace PropostasAdvocaticias.Domain.Entities;

public class Client
{
    private Client() { }
    
    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [StringLength(100, MinimumLength = 5,
        ErrorMessage = "O nome completo deve ter entre 5 e 100 caracteres.")]
    [RegularExpression(
        @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$",
        ErrorMessage = "Use apenas letras e espaços no nome.")]
    public string FullName { get; private set; } = string.Empty;
    
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; private set; } = String.Empty;
    
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
        ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal? Price { get; private set; }

    [Range(0, 100, ErrorMessage = "O desconto deve ser de 0 a 100 %.")]
    public int Discount { get; private set; } = 0;
    
    public DateTime? Date { get; private set; } = DateTime.Now;
    
    
    
    
}