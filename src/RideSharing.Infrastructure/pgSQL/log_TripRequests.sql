-- This is written for PostgreSQL; Lang: PL/pgSQL

-- create a function first for log insertion
CREATE OR REPLACE FUNCTION log_TripRequests()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS
	$$
	BEGIN
		INSERT INTO "TripRequestLogs"("Id", "CustomerId", "Source", "Destination", "CabType", "PaymentMethod", "Status", "TripRequestId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "DeletedAt", "DeletedBy")
		VALUES(gen_random_uuid(), NEW."CustomerId", NEW."Source", NEW."Destination", NEW."CabType", NEW."PaymentMethod", NEW."Status", NEW."Id", NEW."CreatedAt", NEW."CreatedBy", NEW."UpdatedAt", NEW."UpdatedBy", NEW."DeletedAt", NEW."DeletedBy");
	RETURN NEW;
	END;
	$$

-- create a trigger that uses the log insertion function to insert logs
CREATE OR REPLACE TRIGGER log_TripRequests
	AFTER INSERT OR UPDATE OR DELETE
	ON "TripRequests"
	FOR EACH ROW
	EXECUTE PROCEDURE log_triprequests();

