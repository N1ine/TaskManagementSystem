using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.DTOs;
using AutoMapper;

namespace TaskManagement.Application.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateTodoAsync(TodoItem todoItem)
    {
        await _todoRepository.AddAsync(todoItem);
        await _unitOfWork.SaveChangesAsync();

        return todoItem.Id;
    }

    public async Task<List<TodoItem>> GetAllByUserIdAsync(Guid userId)
    {
        return await _todoRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<bool> UpdateAsync(Guid id, Guid userId, UpdateTodoRequest request)
    {
        var todoItem = await _todoRepository.GetByIdAndUserIdAsync(id, userId);

        if (todoItem == null)
        {
            return false;
        }
        
        _mapper.Map(request, todoItem);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var isDeleted = await _todoRepository.DeleteByIdAndUserIdAsync(id, userId);

        if (!isDeleted)
        {
            return false;
        }

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<PagedResult<TodoItemResponse>> GetUserTodosAsync(Guid userId, int page, int pageSize)
    {
        var paginatedList = await _todoRepository.GetPagedAsync(userId, page, pageSize);
        
        var todoResponses = _mapper.Map<List<TodoItemResponse>>(paginatedList.Items);

        return new PagedResult<TodoItemResponse>(
            todoResponses,
            paginatedList.TotalCount,
            paginatedList.PageNumber,
            pageSize
        );
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _todoRepository.GetByIdAndUserIdAsync(id, userId);
    }
}
