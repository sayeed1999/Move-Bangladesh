-- create a function first for log insertion
CREATE OR REPLACE FUNCTION log_Trips()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS
	$$
	BEGIN
		INSERT INTO "TripLogs"("Id", "CustomerId", "DriverId", "TripStatus", "Source", "Destination", "CabType", "PaymentMethod", "TripRequestId", "TripId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "DeletedAt", "DeletedBy")
		VALUES(gen_random_uuid(), NEW."CustomerId", NEW."DriverId", NEW."TripStatus", NEW."Source", NEW."Destination", NEW."CabType", NEW."PaymentMethod", NEW."TripRequestId", NEW."Id", NEW."CreatedAt", NEW."CreatedBy", NEW."UpdatedAt", NEW."UpdatedBy", NEW."DeletedAt", NEW."DeletedBy");
	RETURN NEW;
	END;
	$$

-- create a trigger that uses the log insertion function to insert logs
CREATE OR REPLACE TRIGGER log_Trips
	AFTER INSERT OR UPDATE OR DELETE
	ON "Trips"
	FOR EACH ROW
	EXECUTE PROCEDURE log_trips();
	
-- testing functionality
select * from public."Trips";
select * from public."TripLogs";
