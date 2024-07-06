package transitionchecker

import (
	"ride-sharing-processor/internal/features/transition-checker/trip"
	trip_request "ride-sharing-processor/internal/features/transition-checker/trip-request"

	"github.com/gin-gonic/gin"
)

func InitTransitionChecker(r *gin.Engine) {
	trip.InitTripEndpoint(r)
	trip_request.InitTripRequestEndpoint(r)

}
