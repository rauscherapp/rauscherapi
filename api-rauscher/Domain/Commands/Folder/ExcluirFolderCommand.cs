using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirFolderCommand : FolderCommand
  {
    public Guid CdFolder { get; set; }
    public ExcluirFolderCommand(Guid cdFolder)
    {
      CdFolder = cdFolder;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirFolderCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
