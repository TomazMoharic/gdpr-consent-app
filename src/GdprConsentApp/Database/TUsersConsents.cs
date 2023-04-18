using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdprConsentApp.Database;

[Table("users_consents")]
public class TUsersConsents
{
    [Key]
    [Column("id_user_consent")]
    public int IdUserConsent { get; set; }
    
    [Column("id_user")]
    public int IdUser { get; set; }
    
    [Column("id_consent")]
    public int IdConsent { get; set; }
    
    [ForeignKey("IdUser")]
    public virtual TUsers User { get; set; }
    
    [ForeignKey("IdConsent")]
    public virtual TConsents Consent { get; set; }
}