using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace exam_hall_seating.Repository
{
    public class ClassroomDetailsRepository : IClassroomDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassroomDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ClassroomDetail classroomDetail)
        {
            _context.Add(classroomDetail);
            return Save();
        }

        public List<ClassroomDetail> GetAllDetailById(int id) 
        {
            List<ClassroomDetail> allClasses = _context.ClassroomsDetail.Where(x => x.ClassroomId == id).ToList();
            return allClasses;
        }

        public async Task<int> CreateClassroomBlocksAsync(List<ClassroomDetail> classroomDetail)
        {
            int totalBlock = classroomDetail.Count;
            int totalCapacity = 0;
            for(int i = 0; i < totalBlock; i++)
            {
                if(i == totalBlock - 1 && classroomDetail[i].Column == 2)
                {
                    classroomDetail[i].ValidColumns = "01";
                    totalCapacity += 1 * classroomDetail[i].Row;
                }
                else
                {
                    totalCapacity += TotalValidColumns(classroomDetail[i]);
                }
                await _context.ClassroomsDetail.AddAsync(classroomDetail[i]);
            }
            await _context.SaveChangesAsync(); 
            return totalCapacity;
        }

        public int TotalValidColumns(ClassroomDetail classroomDetail)
        {
            int totalCapacity = 0;
            int totalColumn = classroomDetail.Column;
            for (int currentColumn = 1; currentColumn <= classroomDetail.Column; currentColumn++)
            {
                bool isOdd = currentColumn % 2 == 1;
                bool isValid = isOdd && ((totalColumn - currentColumn) > 1);
                bool isLastColumn = (currentColumn == totalColumn) && (totalColumn != 2);

                if (currentColumn == 1 || isLastColumn || isValid)
                {
                    classroomDetail.ValidColumns += "1";
                    totalCapacity += classroomDetail.Row;
                }
                else
                {
                    classroomDetail.ValidColumns += "0";
                }
            }
            return totalCapacity;
        }

        public bool Delete(ClassroomDetail classroomDetail)
        {
            _context.Remove(classroomDetail);
            return Save();
        }

        public async Task<IEnumerable<ClassroomDetail>> GetAllAsync()
        {
            return await _context.ClassroomsDetail.ToListAsync();
        }

        public async Task<ClassroomDetail> GetByIdAsync(int id)
        {
            return await _context.ClassroomsDetail.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ClassroomDetail classroomDetail)
        {
            _context.Update(classroomDetail);
            return Save();
        }

    }
}
