using CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers;
using CarProjectCQRS.CQRSPattern.Queries.AboutQueries;
using CarProjectCQRS.CQRSPattern.Queries.CarQueries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarProjectCQRS.Controllers
{
    public class MainUiController : Controller
    {
        // About
        private readonly GetAboutByIdQueryHandler _getAboutByIdQueryHandler;
        private readonly GetAboutQueryHandler _getAboutQueryHandler;

        // Car
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly GetCarQueryHandler _getCarQueryHandler;

        // Distance
        private readonly GetDistanceByIdQueryHandler _getDistanceByIdQueryHandler;
        private readonly GetDistanceQueryHandler _getDistanceQueryHandler;

        // Employee
        private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;

        // Feature
        private readonly GetFeatureByIdQueryHandler _getFeatureByIdQueryHandler;
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;

        // Reservation
        private readonly GetReservationByIdQueryHandler _getReservationByIdQueryHandler;
        private readonly GetReservationQueryHandler _getReservationQueryHandler;

        // Service
        private readonly GetServiceByIdQueryHandler _getServiceByIdQueryHandler;
        private readonly GetServiceQueryHandler _getServiceQueryHandler;

        // Slider
        private readonly GetSliderByIdQueryHandler _getSliderByIdQueryHandler;
        private readonly GetSliderQueryHandler _getSliderQueryHandler;

        // Testimonial
        private readonly GetTestimonialByIdQueryHandler _getTestimonialByIdQueryHandler;
        private readonly GetTestimonialQueryHandler _getTestimonialQueryHandler;

        public MainUiController(GetAboutByIdQueryHandler getAboutByIdQueryHandler, GetAboutQueryHandler getAboutQueryHandler, GetCarByIdQueryHandler getCarByIdQueryHandler, GetCarQueryHandler getCarQueryHandler, GetDistanceByIdQueryHandler getDistanceByIdQueryHandler, GetDistanceQueryHandler getDistanceQueryHandler, GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, GetEmployeeQueryHandler getEmployeeQueryHandler, GetFeatureByIdQueryHandler getFeatureByIdQueryHandler, GetFeatureQueryHandler getFeatureQueryHandler, GetReservationByIdQueryHandler getReservationByIdQueryHandler, GetReservationQueryHandler getReservationQueryHandler, GetServiceByIdQueryHandler getServiceByIdQueryHandler, GetServiceQueryHandler getServiceQueryHandler, GetSliderByIdQueryHandler getSliderByIdQueryHandler, GetSliderQueryHandler getSliderQueryHandler, GetTestimonialByIdQueryHandler getTestimonialByIdQueryHandler, GetTestimonialQueryHandler getTestimonialQueryHandler)
        {
            _getAboutByIdQueryHandler = getAboutByIdQueryHandler;
            _getAboutQueryHandler = getAboutQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _getCarQueryHandler = getCarQueryHandler;
            _getDistanceByIdQueryHandler = getDistanceByIdQueryHandler;
            _getDistanceQueryHandler = getDistanceQueryHandler;
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getFeatureByIdQueryHandler = getFeatureByIdQueryHandler;
            _getFeatureQueryHandler = getFeatureQueryHandler;
            _getReservationByIdQueryHandler = getReservationByIdQueryHandler;
            _getReservationQueryHandler = getReservationQueryHandler;
            _getServiceByIdQueryHandler = getServiceByIdQueryHandler;
            _getServiceQueryHandler = getServiceQueryHandler;
            _getSliderByIdQueryHandler = getSliderByIdQueryHandler;
            _getSliderQueryHandler = getSliderQueryHandler;
            _getTestimonialByIdQueryHandler = getTestimonialByIdQueryHandler;
            _getTestimonialQueryHandler = getTestimonialQueryHandler;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AboutList()
        {
            var values = await _getAboutQueryHandler.Handle();
            return View(values);

        }
        public async Task<IActionResult> CarList()
        {
            var values = await _getCarQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> DistanceList()
        {
            var values = await _getDistanceQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _getEmployeeQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> FeatureList()
        {
            var values = await _getFeatureQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> ReservationList()
        {
            var values = await _getReservationQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> ServiceList()
        {
            var values = await _getServiceQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> SliderList()
        {
            var values = await _getSliderQueryHandler.Handle();
            return View(values);
        }
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _getTestimonialQueryHandler.Handle();
            return View(values);
        }


    }
}
