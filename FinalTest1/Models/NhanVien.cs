namespace FinalTest1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        public int Manv { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage ="Bạn phải nhập tên vào")]
        public string Hoten { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tuổi vào")]
        public int? Tuoi { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Bạn phải nhập địa chỉ vào")]
        public string Diachi { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập lương vào")]
        public int? Luong { get; set; }


        public int? Maphong { get; set; }

        public virtual Phong Phong { get; set; }
    }
}
