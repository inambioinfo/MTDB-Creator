#pragma once
#include "clsRegressionPts.h"
#include "clsNormUnifEM.h"
#include <vector>
#include <map> 
using namespace std ; 

namespace RegressionEngine
{
	// Class to perform regression using not a lsq regression but a central regression
	// where everything outside the central mean +- tolerance band is ignored. 

	class clsCentralRegression
	{
		int mint_num_y_bins ; 
		int mint_num_jumps ;

		// number of matches for each x section. 
		int mint_num_section_matches ; 
		// minimum number of points to be present in a section for it to 
		// be considered in computing a function.
		int mint_min_section_pts ; 

		vector<double> mvect_match_scores ; 
		vector<double> mvect_section_mismatch_score ; 
		vector<double> mvect_section_tolerance ; 
		vector<double> mvect_alignment_scores ; 
		vector<int> mvect_best_previous_index ;
		vector<int> mvect_count ; 


		clsNormUnifEM mobj_norm_unif_em ; 

		double mdbl_min_y ; 
		double mdbl_max_y ; 

		// the tolerance applied.
		double mdbl_tolerance ;
		// outlier zscore.
		double mdbl_outlier_zscore ; 


		// variable to store the standard deviations of variable y in each x slice. 
		vector<double> mvect_std_y ; 
		// variable to store alignment function. 
		map<int, int> mmap_alignment_function ; 

		void CalculateMinMax() ; 
		void CalculateSectionsStd() ; 
		void CalculateSectionStdAndCount(int section_num); 
		// this function sets the scores for all possible section matches 
		// assuming that nothing is matched.
		void SetUnmatchedScoreMatrix() ; 
		void CalculateScoreMatrix() ; 
		void CalculateAlignmentMatrix() ; 
		void CalculateRegressionFunction() ; 
		void RemoveOutliersAndCopy(vector<clsRegressionPts> &calib_matches) ; 
	public:
		double mdbl_min_x ; 
		double mdbl_max_x ; 
		int mint_num_x_bins ; 
		vector<clsRegressionPts> mvect_pts ;
		void SetOutlierZScore(double outlier_zscore) ; 
		void PrintScoreMatrix(char *file_name) ; 
		void PrintAlignmentScoreMatrix(char *file_name) ; 
		void PrintRegressionFunction(char *file_name) ; 
		void PrintPoints(char *file_name) ; 
		void Clear() ; 
		clsCentralRegression(void);
		~clsCentralRegression(void);
		void SetOptions(int num_x_bins, int num_y_bins, int num_jumps, double ztolerance) ; 
		double GetPredictedValue(double x) ; 
		void CalculateRegressionFunction(vector<clsRegressionPts> &calib_matches) ; 
		void RemoveRegressionOutliers() ; 
	};
}