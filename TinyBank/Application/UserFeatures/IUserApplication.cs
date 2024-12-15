namespace Application.UserFeatures
{
    public interface IUserApplication
    {
        Task<Guid> CreateAsync(CreateUserDto dto);

        Task<bool?> DeactivateAsync(Guid id);

        Task<UserResponseDto> GetByIdAsync(Guid id);

        Task<IEnumerable<UserResponseDto>> GetAllAsync();

    }
}
