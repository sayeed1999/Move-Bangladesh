package transitionchecker

import (
	"ride-sharing-processor/internal/features/transition-checker/trip"
	trip_request "ride-sharing-processor/internal/features/transition-checker/trip-request"

	"github.com/gin-gonic/gin"
)

func InitEndpoints(r *gin.Engine) {
	rg := r.Group("/api/transition-checker")
	{
		rg.POST("/trip-request-status", trip_request.Handler)
		rg.POST("/trip-status", trip.Handler)
	}
}
