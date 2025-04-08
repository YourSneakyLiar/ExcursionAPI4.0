namespace Domain.Models;

/// <summary>
/// Представляет жалобу (complaint), связанную с пользователем или туром.
/// </summary>
public class Complaint
{
    /// <summary>
    /// Уникальный идентификатор жалобы.
    /// </summary>
    public int ComplaintId { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который подал жалобу.
    /// Может быть null, если жалоба не связана с конкретным пользователем.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Идентификатор тура, к которому относится жалоба.
    /// Может быть null, если жалоба не связана с конкретным туром.
    /// </summary>
    public int? TourId { get; set; }

    /// <summary>
    /// Описание жалобы.
    /// Может быть null, если описание не предоставлено.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Статус жалобы (например, "новая", "в обработке", "решена").
    /// Может быть null, если статус не установлен.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Дата и время создания жалобы.
    /// Может быть null, если дата создания не установлена.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с моделью Tour.
    /// Представляет тур, к которому относится жалоба.
    /// Может быть null, если жалоба не связана с конкретным туром.
    /// </summary>
    public virtual Tour? Tour { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с моделью User.
    /// Представляет пользователя, который подал жалобу.
    /// Может быть null, если жалоба не связана с конкретным пользователем.
    /// </summary>
    public virtual User? User { get; set; }
}