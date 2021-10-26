using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Atalho para propriedade prop + tab
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] Foto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DataAcesso { get; set; } //Este ? é para aceitar nullo, pode nao passar nenhum valor

        [NotMapped]
        public string PasswordString { get; set; }
        

        public List<Personagem> Personagens { get; set; }

        //[Required]
        public string Perfil { get; set; }
        
       



    }
}