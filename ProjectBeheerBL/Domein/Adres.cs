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
        const int MinLengteStraat = 2;
        const int MinLengteGemeente = 2;
        public Adres(string straat, string huisnummer, int postcode, string gemeente)
        {
            Straat = straat;
            Huisnummer = huisnummer;
            Postcode = postcode;
            Gemeente = gemeente;
        }

        private string _straat;
        public string Straat {
            get { return _straat; }
            set {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MinLengteStraat) throw new ProjectException("Straatnaam mag niet leeg of minder dan 2 tekens bevatten");
                var trimmed = value.Trim();
                _straat = trimmed;
            }
        }
        private string _huisnummer;
        public string Huisnummer {
            get { return _huisnummer; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ProjectException("Huisnummer mag niet leeg zijn");

                var trimmed = value.Trim();
                var first = trimmed.FirstOrDefault();
                if (!char.IsDigit(first))
                    throw new ProjectException($"Huisnummer moet met een cijfer beginnen");

                _huisnummer = trimmed;
            }
        }
        private int _postcode;
        public int Postcode {
            get { return _postcode; }
            set {
                if (value >= 1000 && value <= 9999) _postcode = value;
                else throw new ProjectException($"Postcode niet ok");
            }
        }
        private string _gemeente;
        public string Gemeente {
            get { return _gemeente; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Woonplaats mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < MinLengteGemeente) throw new ProjectException($"Woonplaats moet minstens {MinLengteGemeente} karakters hebben");
       
                _gemeente = trimmed;
            }
        }

        public override string ToString()
        {
            return $"{Gemeente}, {Straat}, {Huisnummer}, {Postcode}";
        }
    }
}
