/*MySQL - stored procedure*/
DELIMITER //
DROP PROCEDURE IF EXISTS notedisplay;
CREATE PROCEDURE `notedisplay` ()
BEGIN
	/* Get the combinations of NoteId with their Tag Ids*/
	CREATE TEMPORARY TABLE NAT ( noteid INT , tagname VARCHAR(100));
	INSERT INTO NAT
	SELECT     NoteTags.note_id, TRIM(Tag.text)
	FROM NoteTags INNER JOIN Tag ON NoteTags.tag_id = Tag.tag_id;

	-- Combine the Tags with ':' for the groups of NoteIds
	CREATE TEMPORARY TABLE NTD ( noteid INT , tags VARCHAR(100));
	INSERT INTO NTD
	SELECT noteid, group_concat(tagname separator ':') AS tags
	FROM NAT
	WHERE noteid IS NOT NULL
	GROUP BY noteid;

	/* Join the above to the Notes Table and throw the result*/
	SELECT
		  Notes.note_id AS ID
		, Notes.title AS Title
		, Notes.body AS Text
		, Notes.created AS Created
		, Notes.updated AS Updated
		, NTD.tags AS Tags

	FROM Notes
	LEFT JOIN NTD ON NTD.noteid = Notes.note_id;

	DROP TABLE NAT;
	DROP TABLE NTD;
END //
DELIMITER ;
