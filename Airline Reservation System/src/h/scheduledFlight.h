#ifndef SCHEDULEDFLIGHT_H_INCLUDED
#define SCHEDULEDFLIGHT_H_INCLUDED

// constants for printing details in function get_Int();
#define AIRNAME 6   /* number for airline name   */
#define FROM 7      /* number for departure city */
#define TO 8        /* number for arrival city   */

#define FLIGHTID 3  /* maximum lenght for flight ID             */
#define STATUS 10   /* maximum lenght for status: OK, CANCELLED */




/**************************************************************************************************************************/

/**************************************************************
 *   Sturcture for flight schedules informations
 **************************************************************/

static struct Flight
{
    short flightID;                 // flight's ID
    short departureDate[DATE];      // Departure date
    short timeDeparture;            // time for departure
    short timeArrival;              // time for arrival
    short availableSeats;           // avaliable seats
    char  airline[MAXNAME];         // airline name
    char  departureCity[MAXNAME];   // departure city
    char  arrivalCity[MAXNAME];     // arrival city
    char  status[STATUS];           // status: OK, CANCELLED

}scheduledFlight[MAXPASSENGERS];

/**************************************************************************************************************************/





/**************************************************************************************************************************/

/**************************************************************
 *   Functions prototypes                                     *
 **************************************************************/

void createFlight(struct Flight *scheduledFlight);                              // function to create flight
void modifyFlight(struct Flight *scheduledFlight);                              // function to modify flight
void cancelFlight(struct Flight *scheduledFlight);                              // fuinction to cancel flight


void  show_existingFlights(const struct Flight *scheduledFlight);               // function to show all flights
void  show_FlightInfo(short n, const struct Flight *scheduledFlight);           // function to show a particular flight details
short listFlights(const struct Flight *scheduledFlight);                        // function to list all flights and return number of records
short openfscFlightFile(char *mode);                                            // function to open schedule flight file
short checkID(short flight, short count, const struct Flight *scheduledFlight); // function to chech whether entered ID exist or not
short get_Time(short n);                                                        // function to get time in format dd/mm/yyyy

 /**************************************************************************************************************************/
#endif // SCHEDULEDFLIGHT_H_INCLUDED
