/*MSSQL - stored procedure*/
CREATE PROCEDURE dbo.addattachment
(
	@note_id int,
	@attachment VARBINARY(MAX),
	@filename VARCHAR(100)
)
AS
	/* SET NOCOUNT ON */
BEGIN
/*delete any currently attached note*/
	DELETE FROM Attachment WHERE attach_id = (SELECT attach_id FROM AttachedNotes WHERE note_id = @note_id)

/*insert attachment and filename*/
	DECLARE @AID TABLE( ID INT )
	INSERT INTO Attachment(attachment, filename) OUTPUT inserted.attach_id INTO @AID VALUES(@attachment, @filename)

/*create link*/
	DECLARE @ID INT
	SELECT @ID = ID FROM @AID
	INSERT INTO AttachedNotes(note_id, attach_id) VALUES(@note_id, @ID)
END
	RETURN
