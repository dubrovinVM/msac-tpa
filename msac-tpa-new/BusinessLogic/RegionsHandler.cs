using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace msac_tpa_new.BusinessLogic
{
    public class RegionsHandler
    {
        private static SportmenContext _context;

        public RegionsHandler(SportmenContext context)
        {
            _context = context;
        }

        public SelectList GetSelectedListRegionsWithNonSelection()
        {
            List<Region> regions = _context.Regions.AsNoTracking().ToList();
            regions.Insert(0, new Region { Name = "Всi", Id = 0 });
            return new SelectList(regions, "Id", "Name", 0);
        }

        public SelectList GetSelectedListRegionsWithSelection(int regionId)
        {
            List<Region> regions = _context.Regions.AsNoTracking().ToList();
            return new SelectList(regions, "Id", "Name", regionId);
        }

        public SelectList GetSelectedListRegions()
        {
            List<Region> regions = _context.Regions.AsNoTracking().ToList();
            return new SelectList(regions, "Id", "Name");
        }
    }
}
