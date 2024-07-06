package main

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

type TripRequestStatus int

const (
	NO_DRIVER_FOUND TripRequestStatus = iota + 1
	CUSTOMER_CANCELED
	DRIVER_ACCEPTED
	CUSTOMER_REJECTED_DRIVER
	DRIVER_REJECTED_CUSTOMER
	TRIP_STARTED
	TRIP_REQUEST_REJECTED
)

type TTripRequestStatusTransitionChecker struct {
	tripRequestMap map[TripRequestStatus][]TripRequestStatus
}

func TripRequestStatusTransitionChecker() *TTripRequestStatusTransitionChecker {
	checker := &TTripRequestStatusTransitionChecker{
		tripRequestMap: make(map[TripRequestStatus][]TripRequestStatus),
	}

	checker.tripRequestMap[NO_DRIVER_FOUND] = []TripRequestStatus{
		CUSTOMER_CANCELED,
		DRIVER_ACCEPTED,
	}

	checker.tripRequestMap[CUSTOMER_CANCELED] = []TripRequestStatus{}

	checker.tripRequestMap[DRIVER_ACCEPTED] = []TripRequestStatus{
		CUSTOMER_REJECTED_DRIVER,
		DRIVER_REJECTED_CUSTOMER,
	}

	checker.tripRequestMap[CUSTOMER_REJECTED_DRIVER] = []TripRequestStatus{}

	checker.tripRequestMap[DRIVER_REJECTED_CUSTOMER] = []TripRequestStatus{
		NO_DRIVER_FOUND,
	}

	checker.tripRequestMap[TRIP_STARTED] = []TripRequestStatus{}

	checker.tripRequestMap[TRIP_REQUEST_REJECTED] = []TripRequestStatus{}

	return checker
}

func (checker *TTripRequestStatusTransitionChecker) IsTransitionValid(fromStatus, toStatus TripRequestStatus) (bool, error) {
	supportedStatuses, ok := checker.tripRequestMap[fromStatus]
	if !ok {
		return false, fmt.Errorf("transition from %s to %s is not supported", fromStatus.String(), toStatus.String())
	}

	for _, status := range supportedStatuses {
		if status == toStatus {
			return true, nil
		}
	}

	return false, fmt.Errorf("transition from %s to %s is not allowed", fromStatus.String(), toStatus.String())
}

func (status TripRequestStatus) String() string {
	statusStrings := [...]string{
		"NO_DRIVER_FOUND",
		"CUSTOMER_CANCELED",
		"DRIVER_ACCEPTED",
		"CUSTOMER_REJECTED_DRIVER",
		"DRIVER_REJECTED_CUSTOMER",
		"TRIP_STARTED",
		"TRIP_REQUEST_REJECTED",
	}
	if status < NO_DRIVER_FOUND || status > TRIP_REQUEST_REJECTED {
		return "Unknown"
	}
	return statusStrings[status-1]
}

type TripStatus int

const (
	ONGOING TripStatus = iota + 1
	WAITING_FOR_PAYMENT
	PAYMENT_COMPLETED
)

type ITransitionChecker interface {
	IsTransitionValid(fromStatus TripStatus, toStatus TripStatus) bool
}

type TTripStatusTransitionChecker struct {
	tripMap map[TripStatus][]TripStatus
}

func TripStatusTransitionChecker() *TTripStatusTransitionChecker {
	return &TTripStatusTransitionChecker{
		tripMap: map[TripStatus][]TripStatus{
			ONGOING: {
				WAITING_FOR_PAYMENT,
			},
			WAITING_FOR_PAYMENT: {
				PAYMENT_COMPLETED,
			},
			PAYMENT_COMPLETED: {},
		},
	}
}

func (checker *TTripStatusTransitionChecker) IsTransitionValid(fromStatus TripStatus, toStatus TripStatus) (bool, error) {
	supportedStatuses, ok := checker.tripMap[fromStatus]
	if !ok {
		return false, fmt.Errorf("transition from %s to %s is not supported", fromStatus.String(), toStatus.String())
	}

	for _, supported := range supportedStatuses {
		if supported == toStatus {
			return true, nil
		}
	}

	return false, fmt.Errorf("transition from %s to %s is not allowed", fromStatus.String(), toStatus.String())
}

func (status TripStatus) String() string {
	statusStrings := [...]string{
		"ONGOING",
		"WAITING_FOR_PAYMENT",
		"PAYMENT_COMPLETED",
	}
	if status < ONGOING || status > PAYMENT_COMPLETED {
		return "Unknown"
	}
	return statusStrings[status-1]
}

func main() {
	r := gin.Default()

	// Initialize the transition checker
	tripRequestStatusTransitionChecker := TripRequestStatusTransitionChecker()
	tripStatusTransitionChecker := TripStatusTransitionChecker()

	r.POST("/transition-checker/trip-request-status", func(c *gin.Context) {
		var request struct {
			FromStatus TripRequestStatus `json:"fromStatus"`
			ToStatus   TripRequestStatus `json:"toStatus"`
		}

		// Bind JSON request body into 'request' struct
		if err := c.ShouldBindJSON(&request); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		// Call transition checker
		valid, err := tripRequestStatusTransitionChecker.IsTransitionValid(request.FromStatus, request.ToStatus)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"valid": valid})
	})

	r.POST("/transition-checker/trip-status", func(c *gin.Context) {
		var request struct {
			FromStatus TripStatus `json:"fromStatus"`
			ToStatus   TripStatus `json:"toStatus"`
		}

		// Bind JSON request body into 'request' struct
		if err := c.ShouldBindJSON(&request); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		// Call transition checker
		valid, err := tripStatusTransitionChecker.IsTransitionValid(request.FromStatus, request.ToStatus)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"valid": valid})
	})

	r.Run("localhost:7000")
}
