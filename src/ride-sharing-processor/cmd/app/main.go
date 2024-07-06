package main

import (
	transition_checker "ride-sharing-processor/internal/features/transition-checker"

	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()

	run(r)
}

func run(r *gin.Engine) {
	transition_checker.InitTransitionChecker(r)

	r.Run("localhost:7000")
}
