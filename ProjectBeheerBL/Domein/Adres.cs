using ProjectBeheerBL.Domein.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class Adres
    {
        public Adres(string straat, string huisnummer, int postcode, string gemeente)
        {
            Straat = straat;
            Huisnummer = huisnummer;
            Postcode = postcode;
            Gemeente = gemeente;
        }

        private string straat;
        public string Straat {
            get { return straat; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Straatnaam mag niet leeg zijn");
                var trimmed = value.Trim();
                straat = trimmed;
            }
        }
        private string huisnummer;
        public string Huisnummer {
            get { return huisnummer; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ProjectException("Huisnummer mag niet leeg zijn");

                var trimmed = value.Trim();
                var first = trimmed.FirstOrDefault();
                if (!char.IsDigit(first))
                    throw new ProjectException($"Huisnummer '{value}' moet met een cijfer beginnen");

                huisnummer = trimmed;
            }
        }
        private int postcode;
        public int Postcode {
            get { return postcode; }
            set {
                if (value >= 1000 && value <= 9999) postcode = value;
                else throw new ProjectException($"Postcode {value} niet ok");
            }
        }
        private string gemeente;
        public string Gemeente {
            get { return gemeente; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Woonplaats mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException($"Woonplaats '{value}' moet minstens 2 karakters hebben");
                if (trimmed.Length != value.Length) throw new ProjectException("Woonplaats mag niet met spatie beginnen/eindigen");
                gemeente = trimmed;
            }
        }

        public override string ToString()
        {
            return $"{Gemeente}, {Straat}, {Huisnummer}, {Postcode}";
        }
    }
}
