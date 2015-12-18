/*MySQL - stored procedure*/
DELIMITER //
DROP PROCEDURE IF EXISTS addattachment;
CREATE PROCEDURE `addattachment`
(
	IN noteid INT,
	IN attachment VARBINARY(65000),
	IN filename VARCHAR(100)
)
BEGIN
	DECLARE aid INT;

	/*delete any currently attachmented note*/
	DELETE FROM Attachment WHERE attach_id = (SELECT attach_id FROM AttachedNotes WHERE note_id = noteid);

	/*insert attachmentment and filename*/
	INSERT INTO Attachment(attachment, filename) VALUES(attachment, filename);

	/*create link*/
	SET aid = LAST_INSERT_ID();
	INSERT INTO AttachedNotes(note_id, attach_id) VALUES(noteid, aid);
END //
DELIMITER ;
