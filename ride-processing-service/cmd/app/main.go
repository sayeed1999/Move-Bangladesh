package main

import (
	transition_checker "ride-processing-service/internal/features/transition-checker"

	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()

	run(r)
}

func run(r *gin.Engine) {
	transition_checker.InitEndpoints(r)

	r.Run("localhost:7000")
}
