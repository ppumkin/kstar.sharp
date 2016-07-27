using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kstar.sharp.domain.Extensions;
using kstar.sharp.domain.Enums;


namespace kstar.sharp.domain.Models
{
    public class GridData : InverterDataBase
    {


        public override string ToString()
        {
            string toString = String.Format(@"
Grid:{0}W {1}  rawMode:{2}",
                GridPower, gridMode, gridModeDecimal
            );

            return toString;
        }

        //watts
        public decimal GridPower
        {
            get
            {
                decimal _pGrid = 0;

                switch (gridMode)
                {
                    case GridMode.NONE:
                        _pGrid = 0;
                        break;
                    case GridMode.EXPORT:
                        _pGrid = Decimal.Multiply(Decimal.Round(Decimal.Multiply(Pgrid, 1000), 0), -1);
                        break;
                    case GridMode.IMPORT:
                        _pGrid = Decimal.Round(Decimal.Multiply(Pgrid, 1000), 0);
                        break;
                }

                return _pGrid;
            }
        }


        private decimal Vgrid = 0;
        private decimal Igrid = 0;
        private decimal Pgrid = 0;
        private decimal Fgrid = 0;
        private decimal gridModeDecimal = 0; //1 export? 2 import?
        private GridMode gridMode = GridMode.NONE;

        public GridData(string[] HEXData)
            : base(HEXData)
        {

            try
            {
                Vgrid = Decimal.Divide((HEXData[41] + HEXData[42]).HexToDecimal(), 10);
                Igrid = Decimal.Divide((HEXData[43] + HEXData[44]).HexToDecimal(), 10);
                Pgrid = Decimal.Divide((HEXData[45] + HEXData[46]).HexToDecimal(), 1000);
                Fgrid = Decimal.Divide((HEXData[47] + HEXData[48]).HexToDecimal(), 10);
                //gridModeDecimal = HEXData[49].HexToDecimal(); //1 export? 2 import? //this is always 1 it seems incorrect
                //gridModeDecimal = HEXData[58].HexToDecimal(); //1 export? 2 import? //had to use load mode. 1import 2 export for grid mode
                gridModeDecimal = HEXData[87].HexToDecimal(); //1 export? 2 import? //none of those above are import/export there is a sepreate flag way down the packet about this


                gridMode = (GridMode)gridModeDecimal;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
