using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Take
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long TestId { get; set; }

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual Quiz Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
