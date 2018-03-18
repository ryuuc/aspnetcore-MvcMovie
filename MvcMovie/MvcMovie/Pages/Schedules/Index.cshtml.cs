using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Utilities;

namespace MvcMovie.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly MovieContext _context;
        public IndexModel(MovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public IList<Schedule> Schedule { get; private set; }

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                return Page();
            }

            var publicScheduleData = await FileHelpers.ProcessFromFile(FileUpload.UploadPublicSchedule, ModelState);
            var privateScheduleData = await FileHelpers.ProcessFromFile(FileUpload.UploadPrivateSchedule, ModelState);

            if (!ModelState.IsValid)
            {
                Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                return Page();
            }

            var schedule = new Schedule()
            {
                PublicSchedule = publicScheduleData,
                PublicScheduleSize = FileUpload.UploadPublicSchedule.Length,
                PrivateSchedule = privateScheduleData,
                PrivateScheduleSize = FileUpload.UploadPrivateSchedule.Length,
                Title = FileUpload.Title,
                UploadDT = DateTime.UtcNow
            };

            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}