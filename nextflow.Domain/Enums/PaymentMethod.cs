namespace Nextflow.Domain.Enums;

public enum PaymentMethod : byte
{
    Cash = 1,               // Dinheiro
    CreditCard = 2,         // Cartão de Crédito
    DebitCard = 3,          // Cartão de Débito
    BankTransfer = 4,      // Transferência Bancária
    Pix = 5,               // Pix
    Ticket = 6             // Vale
}
