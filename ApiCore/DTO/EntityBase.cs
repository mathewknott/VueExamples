using System;
using System.ComponentModel.DataAnnotations;
using ApiCore.Models;
using Newtonsoft.Json;

namespace ApiCore.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityBase()
        {
            CreatedUtc = DateTime.UtcNow;
            ModifiedUtc = DateTime.UtcNow;
            Status = Status.Active;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required, JsonIgnore]
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required, JsonIgnore]
        public DateTime ModifiedUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Status Status { get; set; }
    }
}