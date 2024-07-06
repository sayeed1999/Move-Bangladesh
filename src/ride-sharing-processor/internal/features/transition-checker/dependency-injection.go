package transitionchecker

import (
	"ride-sharing-processor/internal/features/transition-checker/trip"
	trip_request "ride-sharing-processor/internal/features/transition-checker/trip-request"

	"github.com/gin-gonic/gin"
)

func InitEndpoints(r *gin.Engine) {
	r.Group("/api/transition-checker")
	{
		r.POST("/trip-request-status", trip_request.Handler)
		r.POST("/trip-status", trip.Handler)
	}
}
