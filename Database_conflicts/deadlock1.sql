use CartoonsDatabase

BEGIN TRANSACTION 
	UPDATE CREATORS SET Name='Creator1'
	WHERE CreatorId=1

	--- DO SOME WORK

	UPDATE CARTOONS SET Channel='MyChannell'
	WHERE CartoonId=1
COMMIT TRANSACTION