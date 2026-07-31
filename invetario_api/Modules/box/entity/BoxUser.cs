using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.box.entity;

[Table("BoxUsers")]
public class BoxUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int boxUserId { get; set; }

    public int boxId { get; set; }

    public int userId { get; set; }

    [ForeignKey("boxId")]
    public Box box { get; set; }

    [ForeignKey("userId")]
    public User user { get; set; }
}
