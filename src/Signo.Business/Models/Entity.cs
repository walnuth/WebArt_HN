using System;
using System.Collections.Generic;
using System.Text;

namespace Signo.Business.Models
{
    public class Entity
    {
        protected Entity()  // contrutor
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }


    }
}
