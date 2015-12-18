/*MSSQL - stored procedure*/
CREATE PROCEDURE dbo.notedisplay
/*this is a slightly modified version of NoteDetails SPROC*/
AS
	/* SET NOCOUNT ON */
BEGIN
	-- Get the combinations of NoteId with their Tag Ids
	DECLARE @NAT TABLE ( noteid int , tagname varchar(100))
	INSERT INTO @NAT
	SELECT     NoteTags.note_id, LTRIM(RTRIM(Tag.text))
	FROM NoteTags INNER JOIN Tag ON NoteTags.tag_id = Tag.tag_id

	-- Combine the Tags with ':' for the groups of NoteIds
	DECLARE @NTD TABLE ( noteid int , tags varchar(100))
	INSERT INTO @NTD
	SELECT
		 nat.noteid, tags = replace
		 ((SELECT tagname AS [data()]
		 FROM @NAT
		 WHERE  noteid = nat.noteid
		 ORDER BY noteid FOR xml path('')), ' ', ':')
	FROM         @NAT nat
	WHERE     nat.noteid IS NOT NULL
	GROUP BY nat.noteid

	-- Join the above to the Notes Table and throw the result
	SELECT
		  Notes.note_id AS ID
		, Notes.title as Title
		, Notes.body as Text
		, Notes.created as Created
		, Notes.updated as Updated
		, ntd.tags as Tags

	FROM @NTD ntd
	INNER JOIN Notes ON ntd.noteid = Notes.note_id

END
RETURN
