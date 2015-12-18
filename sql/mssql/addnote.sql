/*MSSQL - stored procedure*/
CREATE PROCEDURE dbo.addnote
(
	@title nvarchar(50),
	@body nvarchar(MAX),
	@tags nvarchar(25),
	@note_id int output
)

AS
	/* SET NOCOUNT ON */
BEGIN
DECLARE @NID INT
DECLARE @NOTEID TABLE (ID INT)
DECLARE @TID INT
DECLARE @TAGID TABLE (ID INT)
DECLARE @delim CHAR
SET @delim = ':'
DECLARE @start INT
DECLARE @index INT
DECLARE @tag VARCHAR(25)

/*insert a note*/
	BEGIN
		INSERT INTO Notes  (title, body, created)
		OUTPUT INSERTED.note_id INTO @NOTEID
		VALUES  (@title, @body, getdate())
	END

/*split tags*/
	BEGIN
		SET @NID = (SELECT TOP 1 ID from @NOTEID)
		SET @note_id = @NID

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
					/*select @tag, @NID as NOTEID, @TID as TAGID_inserted_in_Tag*/
				END
				ELSE
				BEGIN
					select @TID = Tag.tag_id from Tag where Tag.text = @tag
					/*select @tag, @NID as NOTEID, @TID as TAGID_just_set*/
				END

				/*create note to tag link*/
				BEGIN
					/*select @NID as NOTEID_inserted, @TID as TAGID_inserted_in_NoteTags*/
					INSERT INTO  NoteTags(note_id, tag_id)
					VALUES  (@NID, @TID)
				END
			END
		END
	END
END
RETURN
