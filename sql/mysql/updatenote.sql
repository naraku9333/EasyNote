/*MySQL - stored procedure*/
DELIMITER //
DROP PROCEDURE IF EXISTS updatenote;
CREATE PROCEDURE `updatenote`
(
	IN noteid INT,
	IN title VARCHAR(50),
	IN body VARCHAR(65000),
	IN tags VARCHAR(25)
)

BEGIN
	DECLARE TID INT;
	DECLARE delim CHAR;
	DECLARE strt INT;
	DECLARE idx INT;
	DECLARE tag VARCHAR(25);

	SET delim = ':';

	/*update note*/
	UPDATE Notes  SET title = title, body = body, updated = NOW()
	WHERE note_id = noteid;

	/*delete tag links*/
	DELETE FROM NoteTags WHERE note_id = noteid;

	/*split and add tags*/
	SET strt = 1;
	SET tags = CONCAT(tags, ':');
	SET idx = LOCATE(delim, tags);

	WHILE idx > 0 DO
		SET tag = SUBSTRING(tags, strt, idx - strt);
		SET strt = idx + 1;
		SET idx = LOCATE(delim, tags, strt);

		/*insert tags if not in table*/
		IF NOT EXISTS (select Tag.tag_id from Tag where Tag.text = tag) THEN
			INSERT INTO Tag(Tag.text) VALUES (tag);
			SET TID = LAST_INSERT_ID();
		ELSE
			SET TID = (SELECT Tag.tag_id from Tag where Tag.text = tag);
		END IF;

		/*create note to tag link*/
		INSERT INTO  NoteTags(note_id, tag_id) VALUES  (noteid, TID);
	END WHILE;
END //
DELIMITER ;
