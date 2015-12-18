/*MySQL stored procedure*/
DELIMITER //
DROP PROCEDURE IF EXISTS addnote;
CREATE PROCEDURE `addnote`(	IN title VARCHAR(50), IN body VARCHAR(65000), IN tags VARCHAR(25), OUT nid INT )
BEGIN

	DECLARE delim CHAR(1);
	DECLARE strt INT;
	DECLARE idx INT;
	DECLARE tag VARCHAR(25);
	DECLARE tid INT;

	SET delim = ':';

	/*insert a note*/
	INSERT INTO Notes  (title, body, created)
	VALUES  (title, body, NOW());

	/*split tags*/
	SET nid = LAST_INSERT_ID();

	SET strt = 1;
	SET tags = TRIM(tags);
	IF LENGTH(tags) > 0 THEN
		SET tags = CONCAT(tags, ':');
		SET idx = LOCATE(delim, tags);

		WHILE idx > 0 DO
			SET tag = SUBSTRING(tags, strt, idx - strt);
			SET strt = idx + 1;
			SET idx = LOCATE(delim, tags, strt);

				/*insert tags if not in table*/
				IF NOT EXISTS (select Tag.tag_id from Tag where Tag.text = tag) THEN
					INSERT INTO Tag(Tag.text) VALUES (tag);
					SET tid = LAST_INSERT_ID();
				ELSE
					SET tid = (select Tag.tag_id from Tag where Tag.text = tag);
				END IF;

				/*create note to tag link*/
				INSERT INTO  NoteTags(note_id, tag_id) VALUES  (nid, tid);
		END WHILE;
	END IF;
END //
DELIMITER ;
