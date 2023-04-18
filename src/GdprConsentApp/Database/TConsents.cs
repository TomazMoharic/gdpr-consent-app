using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdprConsentApp.Database;

[Table("consents")]
public class TConsents
{
    [Key]
    [Column("id_consent")]
    public int IdConsent { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("code")]
    public string Code { get; set; }
    
    [Column("consent_duration_days")]
    public int ConsentDurationDays { get; set; }
    
    [Column("created_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedAt { get; set; }
    
    public virtual List<TUsersConsents>? Users { get; set; }
}