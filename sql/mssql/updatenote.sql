/*MSSQL - stored procedure*/
CREATE PROCEDURE dbo.updatenote
(
	@noteid INT,
	@title NVARCHAR(50),
	@body NVARCHAR(MAX),
	@tags NVARCHAR(25)
)

AS
	/* SET NOCOUNT ON */
BEGIN
DECLARE @TID INT
DECLARE @TAGID TABLE (ID INT)
DECLARE @delim CHAR
SET @delim = ':'
DECLARE @start INT
DECLARE @index INT
DECLARE @tag VARCHAR(25)

/*update note*/
	BEGIN
		UPDATE Notes  SET title = @title, body = @body, updated = GETDATE()
		WHERE note_id = @noteid
	END

/*delete tag links*/
	BEGIN
		DELETE FROM NoteTags WHERE note_id = @noteid
	END

/*split and add tags*/
	BEGIN
		SET @start = 1
		SET @tags = @tags + ':'
		SET @index = CHARINDEX(@delim, @tags)

		WHILE(@index > 0)
		BEGIN
			SET @tag = SUBSTRING(@tags, @start, @index - @start)
			SET @start = @index + 1
			SET @index = CHARINDEX(@delim, @tags, @start)

			/*insert tags if not in table*/
			BEGIN
				IF NOT EXISTS(select TOP 1 * from Tag where Tag.text = @tag)
				BEGIN
					INSERT INTO Tag(Tag.text)
					OUTPUT INSERTED.tag_id INTO @TAGID
					VALUES (@tag)
					SELECT @TID = ID FROM @TAGID
				END
				ELSE
				BEGIN
					select @TID = Tag.tag_id from Tag where Tag.text = @tag
				END

				/*create note to tag link*/
				BEGIN
					INSERT INTO  NoteTags(note_id, tag_id)
					VALUES  (@noteid, @TID)
				END
			END
		END
	END
END
RETURN
